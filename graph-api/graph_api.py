import dateutil.parser
import json

class Node(object):
    _deprecated = []
    _aliases = {}
    def __init__(self, raw):
        node_obj = json.loads(raw)
        self._prop_vals = {}
        for prop in self._props:
            self._prop_vals[prop] = self._props[prop].type(node_obj[prop])
    def __getattr__(self, attr):
        if attr in self._deprecated:
            raise DeprecatedAttributeError(attr)
        if attr in self._aliases:
            attr = self._aliases[attr]
        if attr not in self._props:
            raise AttributeError(attr)
        return self._prop_vals[attr]

class Prop(object):
    def __init__(self, type):
        self.type = type

class List(object):
    def __init__(self, subtype):
        self.subtype = subtype
    def __call__(self, raw):
        return [self.subtype(x) for x in raw]

class Fetchable(object):
    def __init__(self, type, fetcher):
        self.type = type
        self.fetcher = fetcher
        self.vals = {}
    def __call__(self, raw):
        if raw not in self.vals:
            self.vals[raw] = self.type(self.fetcher.fetch(raw))
        return self.vals[raw]

class DeprecatedAttributeError(AttributeError):
    pass

def string(raw):
    return raw

def int(raw):
    return raw

def fb_datetime(raw):
    return dateutil.parser.parse(raw)
