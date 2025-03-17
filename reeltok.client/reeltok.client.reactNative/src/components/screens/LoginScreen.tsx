import useAppDimensions from '../../hooks/useAppDimensions'
import { View, StyleSheet, Image, Alert } from 'react-native'
import CustomTextInput from '../input/CustomTextInput'
import CustomButton from '../input/CustomButton'
import React, { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import { LoginRequestDto } from '../../DTOs/login/LoginRequestDto'
import { userLoginThunk } from '../../redux/thunks/authThunks'
import { AppDispatch } from '../../redux/store'
import useAppNavigation from '../../hooks/useAppNavigation'
import useAppSelector from '../../hooks/useAppSelector'

const LoginScreen = () => {
  const navigateToScreen = useAppNavigation()
  const user = useAppSelector((state) => state.users.myUser)

  const dispatch = useDispatch<AppDispatch>()
  const { contentHeight } = useAppDimensions()

  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')

  const handleLogin = () => {
    if (!email || !password) {
      Alert.alert('Error', 'Please enter both email and password.')
      return
    }

    const loginData: LoginRequestDto = { email, password }

    try {
      dispatch(userLoginThunk(loginData)).unwrap()
    } catch (er) {
      Alert.alert('Login Failed', 'Invalid credentials or network error.' + er)
    }
  }

  useEffect(() => {
    if (user?.userId) {
      navigateToScreen('Profile')
    }
  }, [user])

  return (
    <View style={styles.outerContainer}>
      <View style={styles.logoContainer}>
        <Image
          style={styles.logo}
          source={require('./../../../assets/images/icons/ReelTok_3.png')}
        />
      </View>
      <View style={styles.inputContainer}>
        <CustomTextInput
          placeholder="Email.."
          value={email}
          onChangeText={setEmail}
        ></CustomTextInput>
        <CustomTextInput
          placeholder="password.."
          password
          value={password}
          onChangeText={setPassword}
        ></CustomTextInput>
        <CustomButton widthPercentage={0.8} onPress={handleLogin} title="Log ind" />
        <CustomButton
          widthPercentage={0.8}
          transparent
          title="Sign up"
          onPress={() => navigateToScreen('Signup')}
        />
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
