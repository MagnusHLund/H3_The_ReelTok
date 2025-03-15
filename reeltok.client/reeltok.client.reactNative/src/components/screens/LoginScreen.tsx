import useAppDimensions from '../../hooks/useAppDimensions'
import { View, StyleSheet, Image } from 'react-native'
import CustomTextInput from '../input/CustomTextInput'
import CustomButton from '../input/CustomButton'
import React from 'react'

const LoginScreen = () => {
  const { contentHeight } = useAppDimensions()

  return (
    <View style={styles.outerContainer}>
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
          onPress={() => console.log('submitted')}
          title="Log ind"
        ></CustomButton>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  outerContainer: {
    width: '100%',
    height: '100%',
  },
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
    resizeMode: 'contain',
  },
})

export default LoginScreen
