interface CustomTextInputProps {
  password?: boolean
  email?: boolean
  placeholder: string
  borders?: boolean
  widthProcentage?: number
  backgroundColor?: string
}

import React, { useState } from 'react'
import { TextInput, StyleSheet, useWindowDimensions, View } from 'react-native'

const CustomTextInput: React.FC<CustomTextInputProps> = ({
  password,
  email,
  placeholder,
  borders,
  widthProcentage = 0.8,
  backgroundColor = '#565656',
}) => {
  const { width } = useWindowDimensions()
  const [isFocused, setIsFocused] = useState(false)

  const styles = StyleSheet.create({
    input: {
      height: 40,
      borderColor: 'gray',
      borderWidth: borders ? 1 : 0,
      paddingLeft: 10,
      borderRadius: 10,
      color: 'white',
      width: width * widthProcentage,
      backgroundColor: backgroundColor,
    },
    blurredText: {
      color: 'transparent',
      textShadowColor: 'rgba(255, 255, 255, 0.5)',
      textShadowOffset: { width: 0, height: 0 },
      textShadowRadius: 5,
    },
  })

  return (
    <View>
      <TextInput
        style={[
          styles.input,
          email && !isFocused ? styles.blurredText : null,
        ]}
        placeholder={placeholder}
        secureTextEntry={password}
        keyboardType={email ? 'email-address' : 'default'}
        placeholderTextColor={'white'}
        onFocus={() => setIsFocused(true)}
        onBlur={() => setIsFocused(false)}
      />
    </View>
  )
}

export default CustomTextInput
