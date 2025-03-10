import React from 'react'
import ProfileImage from '../profile/ProfileImage'

interface CreatorImageProps {
  profilePictureUrl: string
}

const CreatorImage: React.FC<CreatorImageProps> = ({ profilePictureUrl }) => {
  return (
    <ProfileImage
      source={
        profilePictureUrl
          ? { uri: profilePictureUrl }
          : require('./../../../../assets/images/placeholders/profile-default-img.png')
      }
      width={50}
      height={50}
      allowedToChangePicture={false}
    />
  )
}

export default CreatorImage
