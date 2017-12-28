Graph API Demo
==============

Installation
------------

Using Conda is the easiest way to set up the environment for the
project. `environment.yml` contains the definition, just run `conda env create
-f environment.yml`. If Conda isn't available a Python 2.7 installation with
the listed dependencies in recent versions (check `environment.yml` for exact
versions) should work. Note that the code is not tested outside the included
Conda environment.

- dateutil
- pytz

Usage
-----

Subclass from `graph_api.Node`. There are built-in conversion functions but
you can also write your own. If deprecations are possible, handle
DeprecatedAttributeErrors in client code. See tests for usage examples.

Running tests
-------------

`python test.py`

Commentary/motivation
---------------------

Graph APIs have some dusty corners which can cause issues with a dynamic
object-oriented language like Python. This solution was designed to work around
the gotchas I thought of while sticking to a Pythonic object model.

1. Types in the graph model clash with Python's type model a bit. Edges on the
   graph can connect an object (like a Facebook post) to a simple piece of data
   (like the text of the post) or to another graph node with more edges of its
   own (like comments on the post). In the Facebook API's case, this edges to
   other complex nodes might require another network request.

   This means that a property on the Python object representing an edge could
   either be a simple Python object like a string, integer, or list; or another
   graph edge object which requires a fetch to populate with data.

   This problem is solved by attaching a fetcher object to the node. The
   fetcher accepts a URI and populates the object with the data in its
   fields. Properties on the object can have URIs attached to them, and those
   properties will take care of the necessary fetching for themselves. On the
   other hand, specifying the precise type of properties is unnecessary so the
   API retains the dynamism of Python.

2. Facebook can change the names of their edges or the types of their nodes at
   any time.

   Varying types aren't really a problem for Python code. The lack of
   compile-time warnings can be annoying but a small set of integration tests
   against the real endpoints solves most problems. In many cases, code can be
   written polymorphically and the type changes invisible to consumers of the
   API.

   Name changes are a bigger problem given the lack of a compile phase. The
   mentioned integration tests can catch errors but are unhelpful for
   suggesting solutions to introduced failures. To solve this, each property
   can have an optional list of aliases. This allows simple renames to be
   ignored by client code, and with the next discussed feature provides a
   friendly deprecation path.

3. Javascript's missing property semantics, which many JSON APIs expect clients
   to respect, are entirely different from Python's.

   Javascript objects can contain uninitialized properties. They will have the
   Javascript `null` value. On the other hand, accessing a property which does
   not exist will return `undefined`. In Python, uninitialized properties are
   canonically `None` but throw an exception when they do not exist in their
   parent object at all.

   Thus, idiomatic Javascript code needs to handle both cases, as
   uninitialized properties are sometimes excluded from JSON for size and
   convenience. Deprecation is also pretty painless when clients are expected
   to treat missing data and null data the same way. This expectation is not
   true in Python. Thus, the library preserves the `None`-vs-exception
   dichotomy for Python but allows deprecation to be marked explicitly and
   throws a custom exception in that case.
