import { View, StyleSheet, ImageSourcePropType } from 'react-native'
import CustomButton from '../../input/CustomButton'
import CustomImage from '../common/CustomImage'
import React from 'react'

const showMediaSelector = () => {}


interface ProfileImageProps {
    source: ImageSourcePropType
    }
const ProfileImage : React.FC<ProfileImageProps> = ({ source }) => {
  return (
    <View style={styles.ProfilePicture}>
    <CustomButton onPress={showMediaSelector} transparent>
      <CustomImage
        resizeMode="cover"
        height={80}
        width={80}
        borderRadius={50}
        source={source}
      />
    </CustomButton>
</View>
  )
}
const styles = StyleSheet.create({

    ProfilePicture: {
      flexDirection: 'row',
      justifyContent: 'flex-start',
      alignItems: 'center',
      paddingTop: 60,
    },
  })

export default ProfileImage