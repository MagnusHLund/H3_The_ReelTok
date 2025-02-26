import { View, StyleSheet, Image, useWindowDimensions } from 'react-native'
import React from 'react'
import CustomTextInput from '../input/CustomTextInput'
import CustomButton from '../input/CustomButton'

const LoginScreen = () => {
  const { height, width } = useWindowDimensions()
  return (
    <View style={{ height: height, width: width }}>
      <View style={styles.logoContainer}>
        <Image
          style={styles.logo}
          source={require('./../../../assets/images/icons/ReelTok_3.png')}
        />
      </View>
      <View style={styles.inputContainer}>
        <CustomTextInput placeholder="Email.."></CustomTextInput>
        <CustomTextInput placeholder="password.." password></CustomTextInput>
        <CustomButton
          widthPercentage={0.8}
          onPress={() => console.log('submitited')}
          title="Log ind"
        ></CustomButton>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  inputContainer: {
    flexDirection: 'column',
    marginLeft: '10%',
    top: '20%',
    justifyContent: 'space-evenly',
    height: '30%',
    marginBottom: '-30%',
    maxHeight: '50%',
  },
  logoContainer: {
    display: 'flex',
    alignItems: 'center',
    marginTop: '20%',
  },
  logo: {
    width: 150,
    height: 150,
    resizeMode: 'contain', // Ensure the logo maintains its aspect ratio
  },
})

export default LoginScreen
