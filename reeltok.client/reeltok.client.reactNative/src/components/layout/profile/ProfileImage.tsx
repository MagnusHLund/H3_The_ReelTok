import { View, StyleSheet, ImageSourcePropType } from 'react-native'
import CustomButton from '../../input/CustomButton'
import CustomImage from '../common/CustomImage'
import React, { useState } from 'react'
import Camera from '../camera/Camera'
import useAppNavigation from '../../../hooks/useAppNavigation'

interface ProfileImageProps {
  source: ImageSourcePropType
  height?: number
  width?: number
  allowedToChangePicture: boolean
}

const ProfileImage: React.FC<ProfileImageProps> = ({
  source,
  height = 80,
  width = 80,
  allowedToChangePicture,
}) => {
  const [showCamera, setShowCamera] = useState(false)
  const navigateToScreen = useAppNavigation()

  const handleShowCamera = () => {
    if (allowedToChangePicture) {
      setShowCamera(true)
    } else {
      navigateToScreen('Profile')
    }
  }

  const handleHideCamera = () => {
    setShowCamera(false)
  }

  return (
    <View style={styles.container}>
      {showCamera ? (
        <Camera cameraMode="picture" onClose={handleHideCamera} />
      ) : (
        <View style={styles.ProfilePicture}>
          <CustomButton onPress={handleShowCamera} transparent>
            <CustomImage
              resizeMode="cover"
              height={height}
              width={width}
              borderRadius={50}
              source={source}
            />
          </CustomButton>
        </View>
      )}
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    zIndex: 2,
  },
  ProfilePicture: {
    top: 100,
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'flex-start',
    zIndex: 3,
  },
})

export default ProfileImage
