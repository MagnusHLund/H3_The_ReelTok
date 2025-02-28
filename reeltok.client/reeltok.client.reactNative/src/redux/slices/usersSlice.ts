import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export type UserDetails = {
  userId: string
  username: string
  profileUrl: string
  profilePictureUrl: string
}

export interface UsersProps {
  users: UserDetails[]
}

const initialState: UsersProps = {
  users: [
    {
      userId: 'guidUserId1',
      username: 'Yordan',
      profileUrl: 'someurl.com',
      profilePictureUrl:
        'https://media.licdn.com/dms/image/v2/C4D03AQEnAvXQMx4YiQ/profile-displayphoto-shrink_200_200/profile-displayphoto-shrink_200_200/0/1650551077905?e=2147483647&v=beta&t=XvDyCbvSPMHKmzz9bhdSghsfBx_lN-_ZjJu7UV0dUGk',
    },
    {
      userId: 'guidUserId2',
      username: 'Shazil',
      profileUrl: 'someurl.com',
      profilePictureUrl: 'https://avatars.githubusercontent.com/u/141632242?v=4',
    },
    {
      userId: 'guidUserId3',
      username: 'Magnus',
      profileUrl: 'someurl.com',
      profilePictureUrl: 'https://avatars.githubusercontent.com/u/124877369?v=4',
    },
    {
      userId: 'guidUserId4',
      username: 'Marcus',
      profileUrl: 'someurl.com',
      profilePictureUrl:
        'https://media.licdn.com/dms/image/v2/D4E03AQHYLGtHGjxwJg/profile-displayphoto-shrink_200_200/profile-displayphoto-shrink_200_200/0/1681454525455?e=2147483647&v=beta&t=mcB6X3Gt2cxMEcogEExheyAIMWlu-2L5Zx_7QJWt5b8',
    },
  ],
}

const usersSlice = createSlice({
  name: 'users',
  initialState,
  reducers: {},
})

export const {} = usersSlice.actions
export default usersSlice.reducer
