import { createAsyncThunk } from '@reduxjs/toolkit'
import { LoginRequestDto } from '../../DTOs/login/LoginRequestDto'
import httpService, { HttpMethod, PayloadType } from '../../services/httpService'
import { UserDetails, userLogin, userSignup } from '../slices/usersSlice'
import { Alert } from 'react-native'
import { CreateUserRequestDto } from '../../DTOs/login/CreateUserRequestDto'

export const userLoginThunk = createAsyncThunk(
  'users/userLogin',

  async (loginData: LoginRequestDto, { dispatch }) => {
    const httpMethod: HttpMethod = 'POST'
    const url: string = 'users/login'

    try {
      const userResponse = await httpService<LoginRequestDto>({
        httpMethod,
        url,
        body: loginData,
      })
      if (userResponse?.data && userResponse.data.Success) {
        const loggedInUser: UserDetails = {
          email: '',
          userId: userResponse.data.User.UserId,
          username: userResponse.data.User.UserDetails.Username,
          profilePictureUrl: '',
        }
        dispatch(userLogin([loggedInUser]))
        console.log('Login successfully')
      } else {
        Alert.alert('Login failed: No user found')
      }
    } catch (error) {
      Alert.alert(`Login request failed: ${error}`)
    }
  }
)

export const createUserThunk = createAsyncThunk(
  'users/createUser',
  async (signupData: CreateUserRequestDto, { dispatch }) => {
    const httpMethod: HttpMethod = 'POST'
    const url: string = 'users/signup'
    const payloadType: PayloadType = 'JsonBody'

    try {
      const userResponse = await httpService<CreateUserRequestDto>({
        httpMethod,
        url,
        body: signupData,
        payloadType,
      })

      if (userResponse?.data && userResponse.data.Success) {
        const newUser: UserDetails = {
          email: signupData.email,
          userId: userResponse.data.User.UserId,
          username: signupData.username,
          profilePictureUrl: '',
        }

        dispatch(userSignup(newUser))
        console.log('User created successfully')
      } else {
        Alert.alert('Signup failed: Could not create user')
      }
    } catch (error) {
      Alert.alert(`Signup request failed: ${error}`)
    }
  }
)
