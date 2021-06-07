FROM python:3.8.10
ADD tests.py .

COPY /img /img
RUN make /img
ADD image.py .
ADD index.html .
#to install dependencies:
RUN pip install tornado
EXPOSE 5000

CMD ["python", "./image.py"]