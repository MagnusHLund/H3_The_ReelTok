import ProfileDetails from '../layout/profile/ProfileDetails'
import VideoGallery from '../layout/profile/VideoGallery'
import React from 'react'

const UserProfileScreen: React.FC = () => {
  return (
    <>
      <ProfileDetails />
      <VideoGallery />
    </>
  )
}

export default UserProfileScreen
