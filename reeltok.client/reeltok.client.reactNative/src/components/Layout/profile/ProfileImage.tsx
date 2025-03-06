import { View, StyleSheet, ImageSourcePropType } from 'react-native'
import CustomButton from '../../input/CustomButton'
import CustomImage from '../common/CustomImage'
import React, { useState } from 'react'
import Camera from '../camera/Camera'

interface ProfileImageProps {
  source: ImageSourcePropType
}

const ProfileImage: React.FC<ProfileImageProps> = ({ source }) => {
  const [showCamera, setShowCamera] = useState(false)

  const handleShowCamera = () => {
    setShowCamera(true)
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
              height={80}
              width={80}
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
  },
  ProfilePicture: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'center',
    paddingTop: 60,
  },
})

export default ProfileImage
