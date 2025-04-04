import { UploadedVideo as UploadedVideoType } from '../../../redux/slices/uploadSlice'
import MaterialCommunityIcons from '@expo/vector-icons/MaterialCommunityIcons'
import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import { CameraView, CameraType, useCameraPermissions } from 'expo-camera'
import { setUploadedVideoThunk } from '../../../redux/thunks/uploadThunks'
import { Button, StyleSheet, Text, View, Image } from 'react-native'
import useAppDimensions from '../../../hooks/useAppDimensions'
import useAppDispatch from '../../../hooks/useAppDispatch'
import { useNavigation } from '@react-navigation/native'
import UploadedMedia from '../upload/UploadedMedia'
import AntDesign from '@expo/vector-icons/AntDesign'
import { useState, useRef, useEffect } from 'react'
import CustomButton from '../../input/CustomButton'
import { Entypo } from '@expo/vector-icons'
import useAppNavigation from '../../../hooks/useAppNavigation'

interface CameraProps {
  cameraMode: 'picture' | 'video'
}

export const Camera: React.FC<CameraProps> = ({ cameraMode }) => {
  const ref = useRef<CameraView>(null)
  const [uri, setUri] = useState<UploadedVideoType>({ fileUri: '' })
  const [facing, setFacing] = useState<CameraType>('back')
  const [permission, requestPermission] = useCameraPermissions()
  const [recording, setRecording] = useState<boolean>(false)
  const { contentHeight } = useAppDimensions()
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
  const uploadvideo = async (uri: string | null) => {
    const media = uri
    navigateToScreen('UploadVideo')
  }

  const renderContent = () => {
    return (
      <View style={[styles.contentContainer, { height: contentHeight }]}>
        {cameraMode === 'picture' && uri && (
          <Image
            source={{ uri: uri.fileUri }}
            resizeMode="contain"
            style={[styles.picture, { height: contentHeight }]}
          />
        )}
        {cameraMode === 'video' && uri.fileUri !== '' && <UploadedMedia uri={uri.fileUri} />}
        <View style={styles.contentButtons}>
          <CustomButton onPress={() => setUri({ fileUri: '' })} transparent>
            <MaterialCommunityIcons name="restore" size={50} color={'white'} />
          </CustomButton>
          <CustomButton onPress={() => uploadvideo(uri.fileUri)} transparent>
            <AntDesign name="arrowright" size={50} color="white" />
          </CustomButton>
        </View>
        <View style={styles.closeButton}>
          <CustomButton onPress={navigation.goBack} transparent widthPercentage={0.15}>
            <Entypo name="cross" size={50} color="white" />
          </CustomButton>
        </View>
      </View>
    )
  }

  const renderCamera = () => {
    return (
      <>
        <CameraView style={styles.camera} ref={ref} mode={cameraMode} facing={facing} mirror={true}>
          <View style={styles.contentButtons}>
            <CustomButton onPress={toggleCameraFacing} transparent widthPercentage={0.5}>
              <MaterialCommunityIcons name="camera-flip" size={50} color="white" />
            </CustomButton>
            <CustomButton
              onPress={cameraMode === 'picture' ? takePicture : recordVideo}
              transparent
              widthPercentage={0.5}
            >
              <Entypo name="circle" size={50} color={recording ? 'red' : 'white'} />
            </CustomButton>
          </View>
        </CameraView>
        <View>
          <CustomButton widthPercentage={0.15} onPress={navigation.goBack} transparent>
            <Entypo name="cross" size={50} color="white" />
          </CustomButton>
        </View>
      </>
    )
  }

  return (
    <View style={styles.container}>{uri.fileUri !== '' ? renderContent() : renderCamera()}</View>
  )
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
  contentButtons: {
    position: 'absolute',
    bottom: 44,
    width: '100%',
    alignItems: 'center',
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
})

export default Camera
