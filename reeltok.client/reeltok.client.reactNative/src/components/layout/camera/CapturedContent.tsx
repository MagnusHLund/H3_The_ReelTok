import { View, Image, StyleSheet } from 'react-native'
import UploadedMedia from '../upload/UploadedMedia'
import React from 'react'
import CustomImage from '../common/CustomImage'

interface CapturedContentProps {
  uri: string
  cameraMode: 'picture' | 'video'
  contentHeight: number
  fullWidth: number
}

const CapturedContent: React.FC<CapturedContentProps> = ({
  uri,
  cameraMode,
  contentHeight,
  fullWidth,
}) => {
  return (
    <View style={[styles.contentContainer, { height: contentHeight }]}>
      {cameraMode === 'picture' && uri && (
        <View style={[styles.pictureContainer, { height: contentHeight, width: fullWidth }]}>
          <CustomImage
            source={{ uri }}
            height={contentHeight}
            width={fullWidth}
            borderRadius={0}
          />
        </View>
      )}
      {cameraMode === 'video' && uri !== '' && <UploadedMedia uri={uri} />}
    </View>
  )
}

const styles = StyleSheet.create({
  contentContainer: {
    height: '100%',
    width: '100%',
    position: 'absolute',
    top: 0,
    bottom: 0,
    left: 0,
    right: 0,
    zIndex: 1,
  },
  pictureContainer: {
    width: '100%',
    height: '100%',
    position: 'absolute',
    backgroundColor: 'transparent',
  },
  picture: {
    borderRadius: '50%',
    height: '100%',
  },
})

export default CapturedContent
