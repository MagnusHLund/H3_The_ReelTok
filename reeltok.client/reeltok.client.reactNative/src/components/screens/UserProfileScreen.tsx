import ProfileDetails from '../Layout/profile/ProfileDetails'
import VideoGallery from '../Layout/profile/VideoGallery'
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
