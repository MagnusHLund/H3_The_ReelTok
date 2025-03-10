import { Text } from 'react-native'
import React from 'react'

interface UsernameProps {
  username: string
}

const Username: React.FC<UsernameProps> = ({ username }) => {
  return <Text>{username ? 'intet brugernavn' : ''}</Text>
}

export default Username
