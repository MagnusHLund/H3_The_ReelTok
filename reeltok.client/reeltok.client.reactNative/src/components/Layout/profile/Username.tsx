import { View, Text } from 'react-native'
import React from 'react'


interface UsernameProps {
  username: string
}

const Username: React.FC<UsernameProps> = ({ username }) => {
    return (
        <View>
        <Text>{username}</Text>
        </View>
    )
}
export default Username