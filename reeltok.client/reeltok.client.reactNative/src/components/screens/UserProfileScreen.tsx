import React from 'react'
import { useRoute } from '@react-navigation/native'
import ProfileDetails from '../layout/profile/ProfileDetails'
import VideoGallery from '../layout/profile/VideoGallery'
import { UserDetails } from '../../redux/slices/usersSlice'
import { Text } from 'react-native'

const UserProfileScreen: React.FC = () => {
  const route = useRoute()
  const { userDetails } = route.params as { userDetails: UserDetails }
  console.log(userDetails)
  if (!userDetails) {
    return <Text>User details not found</Text>
  }

  return (
    <>
      <ProfileDetails user={userDetails} />
      <VideoGallery />
    </>
  )
}

export default UserProfileScreen
