import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export type UserDetails = {
  email: string
  userId: string
  username: string
  profilePictureUrl: string
}

export interface UsersProps {
  users: UserDetails[]
  myUser: UserDetails
}

const initialState: UsersProps = {
  users: [],
  myUser: {
    email: '',
    userId: '',
    username: '',
    profilePictureUrl: '',
  },
}

const usersSlice = createSlice({
  name: 'users',
  initialState,
  reducers: {
    userLogin: (state, action: PayloadAction<UserDetails[]>) => {
      state.myUser = action.payload[0]
      state.users = action.payload
    },
    userSignup: (state, action: PayloadAction<UserDetails>) => {
      state.users.push(action.payload)
    },
  },
})

export const { userLogin, userSignup } = usersSlice.actions
export default usersSlice.reducer
