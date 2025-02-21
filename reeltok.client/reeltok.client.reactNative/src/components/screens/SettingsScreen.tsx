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
    { label: 'Bulgarian', value: 'Bulgaran' },
    { label: 'Gaming', value: 'Gaming' },
  ]

  const InputField = ({ label, placeholder }: { label: string; placeholder: string }) => (
    <View>
      <Text style={{ fontSize: 20, fontWeight: '600' }}>{label}</Text>
      <View style={{ height: 50 }}>
        <CustomTextInput placeholder={placeholder} />
      </View>
    </View>
  )

  const logout = "LOGOUT";

  return (
    <View>
      <Text style={styles.ScreenName}>Settings</Text>
      <View style={{ gap: 10, alignItems: 'center' }}>
        <Divider />
        <View>
          <Text style={{ fontSize: 25, fontWeight: '900' }}>User details</Text>
          <InputField label="Username" placeholder="Username" />
          <InputField label="Email" placeholder="Email" />
        </View>
        <Divider />
        <View>
          <Text style={{ fontSize: 25, fontWeight: '900' }}>Language</Text>
          <CustomDropdown categories={Categories} placeholder="English" />
        </View>
        <Divider />
        <View>
          <Text style={{ fontSize: 25, fontWeight: '900' }}>Recommendation</Text>
          <CustomDropdown categories={Categories} placeholder="English" />
        </View>
        <Divider />
        <View>
          <CustomButton widthPercentage={0.8} title={logout} onPress={() => {console.log(logout)}} />
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
    marginBottom: 20,
    fontWeight: '500',
    fontSize: 30,
  },
})
