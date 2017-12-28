import unittest

import graph_api

import datetime
import pytz

class TestFileFetcher(object):
    def fetch(self, uri):
        with open('test_data/' + uri + '.json') as test_file:
            return test_file.read()
tf_fetcher = TestFileFetcher()

class Comment(graph_api.Node):
    _props = {
        'message': graph_api.Prop(graph_api.string)
    }

class Post(graph_api.Node):
    _props = {
        'message': graph_api.Prop(graph_api.string),
        'associated_int': graph_api.Prop(graph_api.int),
        'created_time': graph_api.Prop(graph_api.fb_datetime),
        'comments': graph_api.Prop(graph_api.List(graph_api.Fetchable(Comment, tf_fetcher)))
    }

    _deprecated = ['deprecated_prop']

    _aliases = {'contents': 'message'}

class TestFetch(unittest.TestCase):
    post = Post(tf_fetcher.fetch('post/0'))
    def test_string_property(self):
        self.assertEqual(self.post.message, 'foo')
    def test_int_property(self):
        self.assertEqual(self.post.associated_int, 4)
    def test_datetime_property(self):
        self.assertEqual(self.post.created_time, datetime.datetime(2013, 1, 25, 0, 11, 2, 0, pytz.utc))
    def test_fetchable_property(self):
        self.assertEqual(self.post.comments[0].message, 'bar')
        self.assertEqual(self.post.comments[1].message, 'quux')

class TestDeprecation(unittest.TestCase):
    post = Post(tf_fetcher.fetch('post/0'))
    def test_deprecation(self):
        with self.assertRaises(graph_api.DeprecatedAttributeError):
            self.post.deprecated_prop

class TestRename(unittest.TestCase):
    post = Post(tf_fetcher.fetch('post/0'))
    def test_old_prop(self):
        self.assertEqual(self.post.contents, self.post.message)

if __name__ == '__main__':
    unittest.main()
