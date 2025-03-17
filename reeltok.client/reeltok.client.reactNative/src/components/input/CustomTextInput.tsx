interface CustomTextInputProps {
  password?: boolean
  email?: boolean
  placeholder: string
  borders?: boolean
  widthPercentage?: number
  backgroundColor?: string
  value?: string | null
  onChangeText?: (text: string) => void
}

import React, { useState } from 'react'
import { TextInput, StyleSheet, useWindowDimensions, View } from 'react-native'

const CustomTextInput: React.FC<CustomTextInputProps> = ({
  password,
  email,
  placeholder,
  borders = true,
  widthPercentage = 0.8,
  backgroundColor = 'white',
  value = '',
  onChangeText,
}) => {
  const { width } = useWindowDimensions()
  const [isFocused, setIsFocused] = useState(false)

  return (
    <View>
      <TextInput
        style={[
          styles.input,
          {
            width: width * widthPercentage,
            borderWidth: borders ? 1 : 0,
            backgroundColor: backgroundColor,
          },
          email && !isFocused ? styles.blurredText : null,
        ]}
        placeholder={placeholder}
        secureTextEntry={password}
        keyboardType={email ? 'email-address' : 'default'}
        placeholderTextColor={'black'}
        onFocus={() => setIsFocused(true)}
        onBlur={() => setIsFocused(false)}
        value={value ?? ''}
        onChangeText={onChangeText}
      />
    </View>
  )
}

const styles = StyleSheet.create({
  input: {
    height: 40,
    borderColor: 'gray',
    paddingLeft: 10,
    borderRadius: 10,
    backgroundColor: 'white',
    color: 'black',
  },
  blurredText: {
    color: 'transparent',
    textShadowColor: 'rgba(255, 255, 255, 0.5)',
    textShadowOffset: { width: 0, height: 0 },
    textShadowRadius: 5,
  },
})

export default CustomTextInput
