import { CameraView, CameraType, CameraMode, useCameraPermissions } from 'expo-camera'
import { useState, useRef } from 'react'
import { Button, StyleSheet, Text, TouchableOpacity, useWindowDimensions, View } from 'react-native'
import FontAwesome from '@expo/vector-icons/FontAwesome';
import CustomButton from '../../input/CustomButton';

export const Camera = () => {
  const ref = useRef<CameraView>(null)
  const [uri, setUri] = useState<string | null>(null)
  const [cameraMode, setCameraMode] = useState<CameraMode>('picture')
  const [facing, setFacing] = useState<CameraType>('back')
  const [permission, requestPermission] = useCameraPermissions()
  const [recording, setRecording] = useState<boolean>(false)
  const { height, width } = useWindowDimensions()

  if (!permission) {
    // Camera permissions are still loading.
    return <View />
  }

  if (!permission.granted) {
    // Camera permissions are not granted yet.
    return (
      <View style={styles.container}>
        <Text style={styles.message}>We need your permission to show the camera</Text>
        <Button onPress={requestPermission} title="grant permission" />
      </View>
    )
  }
  // function setCameraMode(){

  // }

  function toggleCameraFacing() {
    setFacing((current) => (current === 'back' ? 'front' : 'back'))
  }

  const takePicture = async () => {
    const photo = await ref.current?.takePictureAsync();
    if (photo?.uri) {
      setUri(photo.uri);
    }
  };

  const recordVideo = async () => {
    if (recording) {
      setRecording(false);
      ref.current?.stopRecording();
    }
  }

  return (
    <View style={[styles.container, { height: height, width: width }]}>
      <CameraView style={styles.camera} facing={facing}>
        <View style={styles.buttonContainer}>
          <TouchableOpacity style={styles.button} onPress={toggleCameraFacing}>
            <Text style={styles.text}>Flip Camera</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <CustomButton onPress={cameraMode ==="picture"? takePicture : recordVideo}><FontAwesome name="video-camera" size={24} color="black" /></CustomButton>
          </TouchableOpacity>
          <TouchableOpacity>

          </TouchableOpacity>
        </View>
      </CameraView>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    backgroundColor: 'red',
  },
  message: {
    textAlign: 'center',
    paddingBottom: 10,
  },
  camera: {
    flex: 1,
  },
  buttonContainer: {
    flex: 1,
    flexDirection: 'row',
    backgroundColor: 'transparent',
    margin: 64,
  },
  button: {
    flex: 1,
    alignSelf: 'flex-end',
    alignItems: 'center',
  },
  text: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
  },
})
export default Camera
