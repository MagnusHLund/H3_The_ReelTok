import { StyleSheet, Text, View } from 'react-native'
import React from 'react'
import CustomTextInput from '../input/CustomTextInput'

const SettingsScreen = () => {
  return (
    <View>
      <Text style={styles.ScreenName}>Settings</Text>
      <View style={{ padding: 20, gap: 10 }}>
        <Text style={{ fontSize: 25 }}>Change user details</Text>
        <View>
          <Text style={{ fontSize: 20 }}>Username</Text>
          <CustomTextInput placeholder="Username"></CustomTextInput>
        </View>
        <Text style={{ fontSize: 25 }}>Change user details</Text>
        <View>
          <Text style={{ fontSize: 20 }}>Username</Text>
          <CustomTextInput placeholder="Username"></CustomTextInput>
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
