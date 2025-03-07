import { View, StyleSheet } from 'react-native'
import SettingsButton from './SettingsButton'
import SubButtons from './SubButtons'
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
          <Username username="Magnus" />
          <SettingsButton />
        </View>
        <View style={styles.LowerContainer}>
          <SubButtons subscriberCount={10} subscriptionCount={30} />
        </View>
      </View>
    </View>
  )
}
const styles = StyleSheet.create({
  outercontainer: {
    flexDirection: 'column',
    // justifyContent: 'center',
    alignItems: 'center',
    width: '100%',
    height: '20%',
  },
  ProfilePictureContainer: {
    width: '100%',
    height: '100%',
    paddingBottom: 20,
  },
  StackedContainer: {
    flexDirection: 'column',
    alignItems: 'center',
    width: '80%',
  },
  UpperContainer: {
    justifyContent: 'space-between',
    flexDirection: 'row',
    marginRight: '20%',
    marginLeft: '50%',
    width: '75%',
    top: '-120%',
  },
  LowerContainer: {
    justifyContent: 'space-between',
    flexDirection: 'row',
    marginRight: '30%',
    marginLeft: '20%',
    width: '80%',
    top: '-40%',
  },
})

export default ProfileDetails
