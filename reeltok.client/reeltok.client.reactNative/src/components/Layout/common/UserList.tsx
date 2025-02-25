import CustomButton from '../../input/CustomButton'
import { StyleSheet, View } from 'react-native'
import UserCard from '../profile/UserCard'
import Divider from './Divider'
import React from 'react'

interface UserListProps {
  users: {
    PictureUrl: string
    ProfileUrl: string
    Username: string
    GuidId: string
  }[]
}

const UserList: React.FC<UserListProps> = ({ users }) => {
  return (
    <View style={{ padding: 10, gap: 10 }}>
      {users.map((user) => (
        <>
          <CustomButton
            key={user.GuidId}
            transparent
            justifyPosition="space-between"
            flexDirection="row"
            widthPercentage={0.95}
            onPress={() => console.log('User clicked' + user.Username)}
          >
            <UserCard user={[{ PictureUrl: user.PictureUrl, Username: user.Username }]} />
          </CustomButton>
          <Divider widthPercentage={100} isGray />
        </>
      ))}
    </View>
  )
}

export default UserList

const styles = StyleSheet.create({})
