import { createAsyncThunk } from '@reduxjs/toolkit'
import { LoginRequestDto } from '../../DTOs/login/LoginRequestDto'
import httpService, { HttpMethod, PayloadType } from '../../services/httpService'
import { UserDetails, userLogin } from '../slices/usersSlice'
import { Alert } from 'react-native'

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
