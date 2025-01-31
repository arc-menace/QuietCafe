import cv2
from ultralytics import YOLO
import time

# Load the YOLO model (yolov8n pretrained on COCO)
model = YOLO('yolov8n.pt')

# Open webcam (use 0 for default webcam)
cap = cv2.VideoCapture(0)

# Check if webcam is opened correctly
if not cap.isOpened():
    print("Error: Could not open webcam.")
    exit()

prev_frame_time = 0

new_frame_time = 0

frameSpeeds = []

# Loop to read frames from webcam
while True:
    ret, frame = cap.read(); 

    if not ret:
        print("Error: Failed to capture frame.")
        break
        
    new_frame_time = time.time()

    fps = 1/(new_frame_time-prev_frame_time) 
    prev_frame_time = new_frame_time 
  
    # converting the fps into integer 
    fps = int(fps) 

    frameSpeeds.append(fps)
  
    # converting the fps to string so that we can display it on frame 
    # by using putText function 
    fps = str(fps) 
  
    # putting the FPS count on the frame 
    cv2.putText(frame, f'FPS {fps} | Average FPS {sum(frameSpeeds) / len(frameSpeeds)}', (7, 70), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2) 

    # Run YOLO model on the frame
    results = model(frame)

    # Extract detections
    detections = results[0].boxes.data.cpu().numpy() if results[0].boxes is not None else []

    for detection in detections:
        x1, y1, x2, y2, score, class_id = detection
        class_id = int(class_id)

        # Check if the detected object is a person (class 0 in COCO)
        if class_id == 0 and score > 0.5:
            # Draw a bounding box around the person
            cv2.rectangle(frame, (int(x1), int(y1)), (int(x2), int(y2)), (0, 255, 0), 2)
            cv2.putText(frame, f'Person: {score:.2f}', (int(x1), int(y1) - 10),
                        cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)

    # Display the frame
    cv2.imshow('Webcam - YOLOv8 Person Detection', frame)

    # Break the loop if 'q' is pressed
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

# Release the webcam and close windows
cap.release()
cv2.destroyAllWindows()