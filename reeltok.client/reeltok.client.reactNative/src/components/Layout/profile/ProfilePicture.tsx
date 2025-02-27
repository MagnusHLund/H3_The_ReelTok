// If the profile picture is clicked on your own profile, you get the option to change it (using the MediaSelector)
// Else you cant interact with the profile picture.
import { Image, StyleSheet } from 'react-native'

interface ProfilePictureProps {
  pictureUrl: string
  width?: number
  height?: number
}

const ProfilePicture: React.FC<ProfilePictureProps> = ({ pictureUrl, width = 35, height = 35 }) => {
  return (
    <Image
      style={[styles.image, { width: width, height: height }]}
      source={{ uri: pictureUrl }}
    />
  )
}

const styles = StyleSheet.create({
  image: {
    borderRadius: 20,
  },
})

export default ProfilePicture
