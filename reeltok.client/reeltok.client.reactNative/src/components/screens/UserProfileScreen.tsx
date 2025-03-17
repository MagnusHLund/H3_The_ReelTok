import React, { useState } from 'react'
import { useRoute } from '@react-navigation/native'
import ProfileDetails from '../layout/profile/ProfileDetails'
import VideoGallery from '../layout/profile/VideoGallery'
import { UserDetails } from '../../redux/slices/usersSlice'
import { Text, TouchableOpacity, View, StyleSheet } from 'react-native'
import Camera from '../layout/camera/Camera'

const UserProfileScreen: React.FC = () => {
  const route = useRoute()
  const { userDetails } = route.params as { userDetails: UserDetails }
  console.log(userDetails)
  if (!userDetails) {
    return <Text>User details not found</Text>
  }

  const [showCamera, setShowCamera] = useState(false)

  const handleShowCamera = () => {
    setShowCamera(true)
  }

  const handleHideCamera = () => {
    setShowCamera(false)
  }

  return (
    <>
      {showCamera ? (
        <View>
          <Camera cameraMode="picture" onClose={handleHideCamera} />
        </View>
      ) : (
        <>
          <TouchableOpacity onPress={handleShowCamera} style={styles.touchable}></TouchableOpacity>
          <ProfileDetails user={userDetails} />
          <VideoGallery />
        </>
      )}
    </>
  )
}
const styles = StyleSheet.create({
  touchable: {
    position: 'absolute',
    left: '7%',
    top: '7.6%',
    height: 80,
    width: 80,
    borderRadius: '50%',
    zIndex: 4,
  },
})
export default UserProfileScreen
