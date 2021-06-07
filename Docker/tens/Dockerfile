From tensorflow/tensorflow:2.5.0
ADD WorkingTF1.py .
COPY /img /img
RUN make /img
ADD index.html .

ADD TFAndServer.py .
ADD hertraind_model ./hertraind_model

#here is the problem with gfile
#/usr/local/lib/python3.6/dist-packages/object_detection/utils/label_map_util.py
RUN pip install tensorflow-object-detection-api
RUN pip install tornado
EXPOSE 5000
CMD ["python", "./TFAndServer.py"]