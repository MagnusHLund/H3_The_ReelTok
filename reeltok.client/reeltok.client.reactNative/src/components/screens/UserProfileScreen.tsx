import ProfileDetails from '../layout/profile/ProfileDetails'
import VideoGallery from '../layout/profile/VideoGallery'
import React from 'react'

const UserProfileScreen: React.FC = () => {
  return (
    <>
      <ProfileDetails
        user={{
          userId: 'guidUserId3',
          username: 'Magnus',
          profileUrl: 'someurl',
          profilePictureUrl: 'https://avatars.githubusercontent.com/u/124877369?v=4',
        }}
      />
      <VideoGallery />
    </>
  )
}

export default UserProfileScreen
