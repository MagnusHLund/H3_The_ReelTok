import { CameraView, CameraType, useCameraPermissions } from 'expo-camera'
import { useState, useRef } from 'react'
import { Button, StyleSheet, Text, TouchableOpacity, View, Image } from 'react-native'
import CustomButton from '../../input/CustomButton'
import MaterialCommunityIcons from '@expo/vector-icons/MaterialCommunityIcons'
import useAppDimensions from '../../../hooks/useAppDimensions'

import { Entypo } from '@expo/vector-icons'
import VideoPlayer from './../video/VideoPlayer'

interface CameraProps {
  cameraMode: 'picture' | 'video'
}

export const Camera: React.FC<CameraProps> = ({ cameraMode }) => {
  const ref = useRef<CameraView>(null)
  const [uri, setUri] = useState<string | null>(null)
  const [facing, setFacing] = useState<CameraType>('back')
  const [permission, requestPermission] = useCameraPermissions()
  const [recording, setRecording] = useState<boolean>(false)
  const { contentHeight } = useAppDimensions()

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

  function toggleCameraFacing() {
    setFacing((current) => (current === 'back' ? 'front' : 'back'))
  }

  const takePicture = async () => {
    const photo = await ref.current?.takePictureAsync()
    if (photo?.uri) {
      setUri(photo.uri)
    }
  }

  const recordVideo = async () => {
    if (recording) {
      setRecording(false)
      ref.current?.stopRecording()
    } else {
      setRecording(true)
      const video = await ref.current?.recordAsync()
      if (video?.uri) {
        setUri(video.uri)
      }
    }
  }

  const renderContent = () => {
    console.log(uri)
    return (
      <View style={[styles.contentContainer, { height: contentHeight }]}>
        {cameraMode === 'picture' && uri && (
          <Image
            source={{ uri }}
            resizeMode="contain"
            style={[styles.picture, { height: contentHeight }]}
          />
        )}
        {cameraMode === 'video' && uri !== null && <VideoPlayer uri={uri} />}
        <CustomButton onPress={() => setUri(null)}>
          <MaterialCommunityIcons name="restore" size={24} color={'white'} />
        </CustomButton>
      </View>
    )
  }

  const renderCamera = () => {
    return (
      <CameraView style={styles.camera} ref={ref} mode={cameraMode} facing={facing} mirror={true}>
        <View style={styles.shutterContainer}>
          <TouchableOpacity onPress={toggleCameraFacing} style={styles.flipButton}>
            <MaterialCommunityIcons name="camera-flip" size={24} color="white" />
          </TouchableOpacity>
          <TouchableOpacity
            onPress={cameraMode === 'picture' ? takePicture : recordVideo}
            style={styles.captureButton}
          >
            <Entypo name="circle" size={24} color={recording ? 'red' : 'white'} />
          </TouchableOpacity>
        </View>
      </CameraView>
    )
  }

  return <View style={styles.container}>{uri ? renderContent() : renderCamera()}</View>
}

const styles = StyleSheet.create({
  container: {
    height: '100%',
    width: '100%',
    backgroundColor: 'black',
    alignItems: 'center',
    justifyContent: 'center',
  },
  message: {
    textAlign: 'center',
    paddingBottom: 10,
    color: 'white',
  },
  camera: {
    flex: 1,
    width: '100%',
  },
  shutterContainer: {
    position: 'absolute',
    bottom: 44,
    left: 0,
    width: '100%',
    alignItems: 'center',
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingHorizontal: 30,
  },
  flipButton: {
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    padding: 10,
    borderRadius: 50,
  },
  captureButton: {
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    padding: 10,
    borderRadius: 50,
  },
  contentContainer: {
    height: '100%',
    width: '100%',
  },
  picture: {
    width: 500,
    height: 300,
  },
  text: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
  },
})

export default Camera
