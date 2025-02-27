import { createSelector } from 'reselect'
import { RootState } from '../store'

const selectUsersState = (state: RootState) => state.users.users

export const selectUserById = createSelector(
  [selectUsersState, (state: RootState, userId: string) => userId],
  (users, userId) => users.find((user) => user.userId === userId)
)
