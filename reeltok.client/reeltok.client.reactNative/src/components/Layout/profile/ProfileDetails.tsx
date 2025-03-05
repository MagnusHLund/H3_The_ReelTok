import { View, StyleSheet } from 'react-native'
import SettingsButton from './SettingsButton'
import ProfileButtons from './ProfileButtons'
import ProfileImage from './ProfileImage'
import Username from './Username'
import React from 'react'

const ProfileDetails = () => {
  return (
    <View style={styles.outercontainer}>
      <View style={styles.ProfilePictureContainer}>
        <ProfileImage
          source={require('./../../../../assets/images/placeholders/profile-default-img.png')}
        />
      </View>
      <View style={styles.StackedContainer}>
        <View style={styles.UpperContainer}>
          <Username username="fffefefefefefefefefefefefefe" />
          <SettingsButton />
        </View>
        <View style={styles.LowerContainer}>
          <ProfileButtons />
        </View>
      </View>
    </View>
  )
}
const styles = StyleSheet.create({
  outercontainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    width: '100%',
  },
  ProfilePictureContainer: {
    width: '20%',
    height: '100%',
    paddingBottom: 20,
  },
  StackedContainer: {
    flexDirection: 'column',
    // justifyContent: 'flex-start',
    alignItems: 'center',
    width: '80%',
  },
  UpperContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'baseline',
    width: '80%',
    marginLeft: '20%',
    marginRight: '20%',
  },
  LowerContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    top: '60%',
  },
})

export default ProfileDetails
