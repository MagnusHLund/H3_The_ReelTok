import React from 'react'
import { Ionicons } from '@expo/vector-icons'
import { StyleSheet, View } from 'react-native'
import CustomButton from '../../input/CustomButton'
import { useNavigation } from '@react-navigation/native'
import { NativeStackNavigationProp } from '@react-navigation/native-stack'

const BackButton = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>()

  const handleOnPress = () => {
    if (navigation.canGoBack()) {
      navigation.goBack()
    }
  }

  return (
    <View style={styles.backButton}>
      <CustomButton widthPercentage={0.1} onPress={handleOnPress}>
        <Ionicons name="arrow-back" size={24} color="black" />
      </CustomButton>
    </View>
  )
}

export default BackButton

const styles = StyleSheet.create({
  backButton: {
    position: 'absolute',
    top: 45, // Adjust for status bar height if necessary
    left: 20,
    zIndex: 10,
    padding: 10,
  },
})
