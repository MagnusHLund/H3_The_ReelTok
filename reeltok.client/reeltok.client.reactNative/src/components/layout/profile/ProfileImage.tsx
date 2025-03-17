import { View, StyleSheet, ImageSourcePropType } from 'react-native'
import CustomButton from '../../input/CustomButton'
import CustomImage from '../common/CustomImage'
import React, { useState } from 'react'
import Camera from '../camera/Camera'
import useAppNavigation from '../../../hooks/useAppNavigation'
import { UserDetails } from '../../../redux/slices/usersSlice'

interface ProfileImageProps {
  source: ImageSourcePropType
  height?: number
  width?: number
  allowedToChangePicture: boolean
  user?: UserDetails
}

const ProfileImage: React.FC<ProfileImageProps> = ({
  source,
  height = 80,
  width = 80,
  allowedToChangePicture,
  user,
}) => {
  return (
    <View style={styles.container}>
      <View style={styles.ProfilePicture}>
        <CustomImage
          resizeMode="cover"
          height={height}
          width={width}
          borderRadius={50}
          source={source}
        />
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    zIndex: 2,
  },
  ProfilePicture: {
    flexDirection: 'row',
    zIndex: 3,
  },
})

export default ProfileImage
