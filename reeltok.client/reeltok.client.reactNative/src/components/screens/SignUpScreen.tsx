import { View, StyleSheet, Image, useWindowDimensions, Alert } from 'react-native'
import CustomTextInput from '../input/CustomTextInput'
import CustomDropdown, { DropdownOption } from '../input/CustomDropdown'
import CustomButton from '../input/CustomButton'
import React, { useEffect, useState } from 'react'
import useAppNavigation from '../../hooks/useAppNavigation'
import useAppSelector from '../../hooks/useAppSelector'
import { AppDispatch } from '../../redux/store'
import { useDispatch } from 'react-redux'
import { CreateUserRequestDto } from '../../DTOs/login/CreateUserRequestDto'

const Categories = [
  { key: 1, label: 'Gaming', value: 'Gaming' },
  { key: 2, label: 'Tech', value: 'Tech' },
  { key: 3, label: 'Dance', value: 'Dance' },
  { key: 4, label: 'Fight', value: 'Fight' },
  { key: 5, label: 'Sport', value: 'Sport' },
  { key: 6, label: 'Comedy', value: 'Comedy' },
]

const SignUpScreen = () => {
  const navigateToScreen = useAppNavigation()
  const user = useAppSelector((state) => state.users.myUser)

  const [email, setEmail] = useState('')
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [interest, setInterest] = useState(0)
  const dispatch = useDispatch<AppDispatch>()

  const handleChangeCategory = (selectedCategory: DropdownOption) => {
    setInterest(selectedCategory.key)
  }

  const handleSignup = () => {
    // if (!email || !password) {
    //   Alert.alert('Error', 'Please enter both email and password.')
    //   return
    // }

    console.log(username)
    console.log(email)
    console.log(password)
    console.log(interest)

    // const signup: CreateUserRequestDto = {email, password, username, interest

    // }
  }

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
        <CustomTextInput
          placeholder="Username.."
          value={username}
          onChangeText={setUsername}
        ></CustomTextInput>
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
        <CustomDropdown options={Categories} onChange={handleChangeCategory} />
        <CustomButton
          widthPercentage={0.8}
          onPress={() => console.log('Create user')}
          title="Create User"
        ></CustomButton>
        <CustomButton
          widthPercentage={0.8}
          transparent
          title="Back to login"
          onPress={handleSignup}
        />
      </View>
    </View>
  )
}

export default SignUpScreen
