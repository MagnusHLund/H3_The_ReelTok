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
    return <Text style={styles.errorText}>User details not found</Text>
  }

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
        // <View style={styles.cameraContainer}>
          <Camera cameraMode="picture" onClose={handleHideCamera} />
        // {/* </View> */}
      ) : (
        <>
          <TouchableOpacity onPress={handleShowCamera} style={styles.touchable}></TouchableOpacity>
          <ProfileDetails userId={userDetails.userId} />
          <VideoGallery />
        </>
      )}
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  cameraContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  touchable: {
    position: 'absolute',
    left: '7%',
    top: '7.6%',
    height: 80,
    width: 80,
    borderRadius: 40,
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 4,
  },
  errorText: {
    color: 'red',
    textAlign: 'center',
    marginTop: 20,
  },
})

export default UserProfileScreen
