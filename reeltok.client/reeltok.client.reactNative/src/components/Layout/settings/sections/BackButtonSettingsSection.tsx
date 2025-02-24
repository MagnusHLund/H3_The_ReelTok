import { StyleSheet, Text, View } from 'react-native'
import React from 'react'
import { Ionicons } from '@expo/vector-icons'
import CustomButton from '../../../input/CustomButton'
import { useNavigation } from '@react-navigation/native'
import { NativeStackNavigationProp } from '@react-navigation/native-stack'

const BackButtonSettingsSection = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()

  return (
    <View style={styles.backButton}>
      <CustomButton widthPercentage={0.1} onPress={() => navigation.replace('Profile')}>
        <Ionicons name="arrow-back" size={24} color="black" />
      </CustomButton>
    </View>
  )
}

export default BackButtonSettingsSection

const styles = StyleSheet.create({
  backButton: {
    position: 'absolute',
    top: 45, // Adjust for status bar height if necessary
    left: 20,
    zIndex: 10,
    padding: 10,
  },
})
