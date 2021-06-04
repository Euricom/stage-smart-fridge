import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'    # Suppress TensorFlow logging (1)
import pathlib
import tensorflow as tf
tf.gfile = tf.io.gfile

tf.get_logger().setLevel('ERROR')           # Suppress TensorFlow logging (2)

# Enable GPU dynamic memory allocation
gpus = tf.config.experimental.list_physical_devices('GPU')
for gpu in gpus:
    tf.config.experimental.set_memory_growth(gpu, True)

def download_images():
    base_url = 'https://raw.githubusercontent.com/tensorflow/models/master/research/object_detection/test_images/'
    filenames = ['image1.jpg', 'image2.jpg']
    image_paths = []
    for filename in filenames:
        image_path = tf.keras.utils.get_file(fname=filename,
                                            origin=base_url + filename,
                                            untar=False)
        image_path = pathlib.Path(image_path)
        image_paths.append(str(image_path))
    return image_paths

IMAGE_PATHS = download_images()
PATH_TO_MODEL_DIR = "./hertraind_model/saved_model"
PATH_TO_LABELS = './hertraind_model/labels.pbtxt'


import time
from object_detection.utils import label_map_util
from object_detection.utils import visualization_utils as viz_utils

PATH_TO_SAVED_MODEL = PATH_TO_MODEL_DIR

print('Loading model...', end='')
start_time = time.time()

# Load saved model and build the detection function
model = tf.saved_model.load(PATH_TO_SAVED_MODEL)
detect_fn = model.signatures['serving_default']

end_time = time.time()
elapsed_time = end_time - start_time
print('Done! Took {} seconds'.format(elapsed_time))



category_index = label_map_util.create_category_index_from_labelmap(PATH_TO_LABELS,use_display_name=True)


import numpy as np
from PIL import Image
import matplotlib.pyplot as plt
import warnings
warnings.filterwarnings('ignore')   # Suppress Matplotlib warnings

def load_image_into_numpy_array(path):
    """Load an image from file into a numpy array.

    Puts image into numpy array to feed into tensorflow graph.
    Note that by convention we put it into a numpy array with shape
    (height, width, channels), where channels=3 for RGB.

    Args:
      path: the file path to the image

    Returns:
      uint8 numpy array with shape (img_height, img_width, 3)
    """
    #path = "img/{f.filename}"
    return np.array(Image.open(path))





# sphinx_gallery_thumbnail_number = 2



import tornado.web
import tornado.ioloop

class uploadHandler(tornado.web.RequestHandler):
    def get(self):
        self.render("index.html")
    def post(self):
        files = self.request.files["imgFile"]
        for f in files:
            print(f.filename)
            fh = open(f"img/picture", "wb")
            fh.write(f.body)
            fh.close()
            #here comes the tensorflow Code
            print('Running inference for {}... '.format(f"img/picture"), end='')
            image_np = load_image_into_numpy_array(f"img/picture")

            # Things to try:
            # Flip horizontally
            # image_np = np.fliplr(image_np).copy()

            # Convert image to grayscale
            # image_np = np.tile(
            #     np.mean(image_np, 2, keepdims=True), (1, 1, 3)).astype(np.uint8)

            # The input needs to be a tensor, convert it using `tf.convert_to_tensor`.
            input_tensor = tf.convert_to_tensor(image_np)
            # The model expects a batch of images, so add an axis with `tf.newaxis`.
            input_tensor = input_tensor[tf.newaxis, ...]

            detections = detect_fn(input_tensor)

            # All outputs are batches tensors.
            # Convert to numpy arrays, and take index [0] to remove the batch dimension.
            # We're only interested in the first num_detections.
            num_detections = int(detections.pop('num_detections'))
            detections = {key: value[0, :num_detections].numpy()
                        for key, value in detections.items()}
            detections['num_detections'] = num_detections

            # detection_classes should be ints.
            detections['detection_classes'] = detections['detection_classes'].astype(np.int64)

            image_np_with_detections = image_np.copy()

            viz_utils.visualize_boxes_and_labels_on_image_array(
                image_np_with_detections,
                detections['detection_boxes'],
                detections['detection_classes'],
                detections['detection_scores'],
                category_index,
                use_normalized_coordinates=True,
                max_boxes_to_draw=200,
                min_score_thresh=.30,
                agnostic_mode=False)
            plt.figure()
            plt.imshow(image_np_with_detections)
            print('Done')
            #plt.savefig("C:/Users/matth/Documents/stage/TensorFlow_Goed/foto.png")
            plt.show()
            self.write(str(detections['detection_boxes']))
            self.write(str(detections['detection_classes']))
            self.write(str(detections['detection_scores']))
            










            #here ends the Tensorflow code
            
        

if(__name__ == "__main__"):
    app = tornado.web.Application([
        ("/", uploadHandler),
        ('/img/(.*)', tornado.web.StaticFileHandler, {"path" : "img"})
    ])

    app.listen(5000)
    print("Listening on port 5000")
    tornado.ioloop.IOLoop.instance().start()