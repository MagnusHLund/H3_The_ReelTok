// If the profile picture is clicked on your own profile, you get the option to change it (using the MediaSelector)
// Else you cant interact with the profile picture.
import { Image, StyleSheet } from "react-native";

interface ProfilePictureProps {
  pictureUrl: string,
  width?: number,
  height?: number,
}

const ProfilePicture: React.FC<ProfilePictureProps> = ({ pictureUrl, width, height }) => {

  const styles = StyleSheet.create({
    image: {
      width: width ?? 35,
      height: height ?? 35,
      borderRadius: 20,
    },
  });

  return (
    <Image style={styles.image} source={{ uri: pictureUrl }}/>
  );
};

export default ProfilePicture;
