import { CameraView, CameraType } from 'expo-camera'
import { View, StyleSheet } from 'react-native'
import React from 'react'
import CustomButton from '../../input/CustomButton'
import MaterialCommunityIcons from '@expo/vector-icons/MaterialCommunityIcons'
import { Entypo } from '@expo/vector-icons'

interface CameraViewComponentProps {
  cameraMode: 'picture' | 'video'
  facing: CameraType
  recording: boolean
  toggleCameraFacing: () => void
  takePicture: () => void
  recordVideo: () => void
  fullWidth: number
  contentHeight: number
}

const CameraViewComponent = React.forwardRef<CameraView, CameraViewComponentProps>(
  (
    {
      cameraMode,
      facing,
      recording,
      toggleCameraFacing,
      takePicture,
      recordVideo,
      fullWidth,
      contentHeight,
    },
    ref
  ) => {
    return (
      <CameraView
        style={[styles.camera, { width: fullWidth, height: contentHeight }]}
        ref={ref}
        mode={cameraMode}
        facing={facing}
        mirror={facing === 'front'}
      >
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
    )
  }
)

const styles = StyleSheet.create({
  camera: {
    position: 'absolute',
    top: 0,
    bottom: 0,
    left: 0,
    right: 0,
    zIndex: 1,
  },
  contentButtons: {
    position: 'absolute',
    bottom: '5%',
    width: '100%',
    alignItems: 'center',
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
})

export default CameraViewComponent
