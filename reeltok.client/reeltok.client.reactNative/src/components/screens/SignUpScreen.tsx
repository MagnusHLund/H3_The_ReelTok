import { View, StyleSheet, Image, useWindowDimensions } from 'react-native'
import React from 'react'
import CustomTextInput from '../input/CustomTextInput'
import CustomButton from '../input/CustomButton'
import CustomDropdown from '../input/CustomDropdown'

const Categories = [
  { label: 'Gaming', value: 'Gaming' },
  { label: 'Tech', value: 'Tech' },
  { label: 'Dance', value: 'Dance' },
  { label: 'Fight', value: 'Fight' },
  { label: 'Sport', value: 'Sport' },
  { label: 'Comedy', value: 'Comedy' },
]

const SignUpScreen = () => {
  const { height, width } = useWindowDimensions()
  const styles = StyleSheet.create({
    container: {
      height: height,
      width: width,
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
      resizeMode: 'contain', // Ensure the logo maintains its aspect ratio
    },
  })
  return (
    <View style={styles.container}>
      <View style={styles.logoContainer}>
        <Image
          style={styles.logo}
          source={require('./../../../assets/images/icons/ReelTok_3.png')}
        />
      </View>
      <View style={styles.inputContainer}>
        <CustomTextInput placeholder="Email.."></CustomTextInput>
        <CustomTextInput placeholder="password.." password></CustomTextInput>
        <CustomDropdown placeholder="Vælg en kategori" categories={Categories}></CustomDropdown>
        <CustomButton
          widthPercentage={0.8}
          onPress={() => console.log('Create user')}
          title="Opret bruger"
        ></CustomButton>
        <CustomButton
          widthPercentage={0.8}
          onPress={() => console.log('already account')}
          title="Login hvis du allerede har en bruger"
        ></CustomButton>
      </View>
    </View>
  )
}

export default SignUpScreen
