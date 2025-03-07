import { StyleSheet, Text, View } from 'react-native'
import ProfilePicture from './../common/ProfilePicture'
import React from 'react'

interface UserCardProps {
  user: {
    PictureUrl: string
    Username: string
  }[]
}

const UserCard: React.FC<UserCardProps> = ({ user }) => {
  return (
    <>
      {user.map((user, index) => (
        <View key={index} style={styles.container}>
          <ProfilePicture pictureUrl={user.PictureUrl} width={75} height={75} />
          <Text style={styles.textfield} numberOfLines={1} ellipsizeMode="tail">
            {user.Username}
          </Text>
        </View>
      ))}
    </>
  )
}

export default UserCard

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    gap: 10,
    alignItems: 'center',
    flex: 1,
  },
  textfield: {
    fontSize: 20,
    fontWeight: '500',
    flexShrink: 1,
    flex: 1,
  },
})
