import { UploadedVideo as UploadedVideoType } from '../../../redux/slices/uploadSlice'
import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import { CameraView, CameraType, useCameraPermissions } from 'expo-camera'
import { setUploadedVideoThunk } from '../../../redux/thunks/uploadThunks'
import { Button, StyleSheet, Text, View } from 'react-native'
import useAppDimensions from '../../../hooks/useAppDimensions'
import useAppDispatch from '../../../hooks/useAppDispatch'
import { useNavigation } from '@react-navigation/native'
import { useState, useRef, useEffect } from 'react'
import useAppNavigation from '../../../hooks/useAppNavigation'
import CameraViewComponent from './CameraViewComponent'
import CameraControls from './CameraControls'
import CapturedContent from './CapturedContent'
import CloseButton from './CloseButton'

interface CameraProps {
  cameraMode: 'picture' | 'video'
  onClose: () => void
}

export const Camera: React.FC<CameraProps> = ({ cameraMode, onClose }) => {
  const ref = useRef<CameraView>(null)
  const [uri, setUri] = useState<UploadedVideoType>({ fileUri: '' })
  const [facing, setFacing] = useState<CameraType>('back')
  const [permission, requestPermission] = useCameraPermissions()
  const [recording, setRecording] = useState<boolean>(false)
  const { contentHeight, fullHeight, fullWidth } = useAppDimensions()
  const navigateToScreen = useAppNavigation()
  const navigation = useNavigation<NativeStackNavigationProp<any>>()
  const dispatch = useAppDispatch()

  useEffect(() => {
    dispatch(setUploadedVideoThunk(uri))
  }, [uri])

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
      setUri({ fileUri: photo.uri })
    }
  }

  const handleNavigate = () => {
    if (cameraMode === 'picture') {
      navigateToScreen('Profile')
    } else {
      navigation.goBack()
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
        setUri({ fileUri: video.uri })
      }
    }
  }

  const uploadVideo = async (uri: string | null) => {
    onClose() // Call the onClose callback to unmount the Camera component
    navigateToScreen('UploadVideo')
  }

  const renderContent = () => {
    return (
      <View style={[styles.contentContainer, { height: contentHeight, width: fullWidth }]}>
        <CapturedContent
          uri={uri.fileUri}
          cameraMode={cameraMode}
          contentHeight={contentHeight}
          fullWidth={fullWidth}
        />
        <CameraControls
          uri={uri.fileUri}
          uploadVideo={uploadVideo}
          resetUri={() => setUri({ fileUri: '' })}
        />
        <CloseButton onClose={onClose} />
      </View>
    )
  }

  const renderCamera = () => {
    return (
      <View>
        <CameraViewComponent
          ref={ref}
          cameraMode={cameraMode}
          facing={facing}
          recording={recording}
          toggleCameraFacing={toggleCameraFacing}
          takePicture={takePicture}
          recordVideo={recordVideo}
          fullWidth={fullWidth}
          contentHeight={contentHeight}
        />
        <CloseButton onClose={onClose} />
      </View>
    )
  }

  return (
    <View style={[styles.container, { height: contentHeight, width: fullWidth }]}>
      {uri.fileUri !== '' ? renderContent() : renderCamera()}
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: 'black',
    zIndex: 1,
    position: 'absolute',
  },
  message: {
    textAlign: 'center',
    paddingBottom: 10,
    color: 'white',
  },
  contentContainer: {
    backgroundColor: 'black',
    alignItems: 'center',
    justifyContent: 'center',
    zIndex: 1,
    position: 'absolute',
  },
})

export default Camera
