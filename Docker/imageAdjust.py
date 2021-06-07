import tornado.web
import tornado.ioloop
import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'    # Suppress TensorFlow logging (1)
import pathlib
import tensorflow as tf
tf.gfile = tf.io.gfile

class uploadHandler(tornado.web.RequestHandler):
    def get(self):
        self.render("index.html")
    def post(self):
        files = self.request.files["imgFile"]
        for f in files:
            fh = open(f"img/{f.filename}", "wb")
            fh.write(f.body)
            fh.close()
        self.write(f"http://localhost:5000/img/{f.filename}")

if(__name__ == "__main__"):
    app = tornado.web.Application([
        ("/", uploadHandler),
        ('/img/(.*)', tornado.web.StaticFileHandler, {"path" : "img"})
    ])

    app.listen(5000)
    print("Listening on port 5000")
    tornado.ioloop.IOLoop.instance().start()