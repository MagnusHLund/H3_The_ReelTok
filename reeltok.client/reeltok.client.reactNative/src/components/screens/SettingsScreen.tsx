import { StyleSheet, Text, View } from 'react-native'
import React from 'react'
import CustomTextInput from '../input/CustomTextInput'
import Divider from '../Layout/common/Divider'
import CustomDropdown from '../input/CustomDropdown'
import CustomButton from '../input/CustomButton'

const SettingsScreen = () => {
  const Categories = [
    { label: 'English', value: 'English' },
    { label: 'Danish', value: 'Danish' },
    { label: 'Chinese', value: 'Chinese' },
    { label: 'Bulgarian', value: 'Bulgarian' },
  ]

  return (
    <View>
      <Text style={styles.ScreenName}>Settings</Text>
      <View style={{ gap: 10, padding: 20 }}>
        <Divider />
        <Text style={{ fontSize: 25, fontWeight: '900' }}>Change user details</Text>
        <View>
          <Text style={{ fontSize: 20, fontWeight: '600' }}>Username</Text>
          <View style={{ height: 50 }}>
            <CustomTextInput placeholder="Username"></CustomTextInput>
          </View>
        </View>
        <View>
          <Text style={{ fontSize: 20, fontWeight: '600' }}>Username</Text>
          <View style={{ height: 50 }}>
            <CustomTextInput placeholder="Username" />
          </View>
        </View>
        <Divider />
        <Text style={{ fontSize: 25, fontWeight: '900' }}>Language</Text>
        <View>
          <Text style={{ fontSize: 20, fontWeight: '600' }}>Select Language</Text>
          <CustomDropdown label="" categories={Categories} placeholder="English" />
        </View>
        <Divider />
        <Text style={{ fontSize: 25, fontWeight: '900' }}>Logout</Text>
        <View>
          <CustomButton onPress={() => {}} />
        </View>
      </View>
    </View>
  )
}

export default SettingsScreen

const styles = StyleSheet.create({
  ScreenName: {
    textAlign: 'center',
    marginTop: 50,
    fontWeight: '500',
    fontSize: 30,
  },
})
