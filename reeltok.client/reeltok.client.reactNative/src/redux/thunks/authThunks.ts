import { createAsyncThunk } from '@reduxjs/toolkit'
import { LoginRequestDto } from '../../DTOs/login/LoginRequestDto'
import httpService, { HttpMethod } from '../../services/httpService'
import { UserDetails, userLogin } from '../slices/usersSlice'
import { Alert } from 'react-native'

export const userLoginThunk = createAsyncThunk(
  'users/userLogin',

  async (loginData: LoginRequestDto, { dispatch }) => {
    const method: HttpMethod = 'POST'
    const url: URL = new URL('https://api.reeltok.site/api/users/login')

    try {
      const userResponse = await httpService<LoginRequestDto>({ method, url, body: loginData })
      if (userResponse?.data && userResponse.data.Success) {
        const loggedInUser: UserDetails = {
          email: '',
          userId: userResponse.data.User.UserId,
          username: userResponse.data.User.UserDetails.Username,
          profilePictureUrl: '', // Assuming profilePictureUrl is not provided in the response
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
