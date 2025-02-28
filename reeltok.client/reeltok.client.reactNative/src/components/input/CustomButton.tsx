import { TouchableOpacity, StyleSheet, Text, useWindowDimensions } from 'react-native'
import { Poppins_400Regular } from '@expo-google-fonts/poppins'
import { useFonts } from 'expo-font'
import { ReactNode } from 'react'

interface ButtonProps {
  title?: string
  children?: ReactNode
  transparent?: boolean
  widthPercentage?: number | 'auto'
  flexDirection?: 'row' | 'column'
  borders?: boolean
  justifyPosition?: 'center' | 'flex-start' | 'flex-end' | 'space-between' | 'space-around'
  onPress: () => void
}

const CustomButton: React.FC<ButtonProps> = ({
  children,
  title,
  transparent,
  widthPercentage = 0.33,
  flexDirection,
  borders,
  justifyPosition = 'center',
  onPress,
}) => {
  useFonts({
    Poppins_400Regular,
  })
  const { width } = useWindowDimensions()
  const calculatedWidth = widthPercentage === 'auto' ? 'auto' : width * widthPercentage

  return (
    <TouchableOpacity
      style={[
        buttonStyling.button,
        {
          borderWidth: borders ? 2 : 0,
          backgroundColor: transparent ? 'transparent' : '#ff0050',
          width: calculatedWidth,
          flexDirection: flexDirection,
          justifyContent: justifyPosition,
        },
      ]}
      activeOpacity={1}
      onPress={onPress}
    >
      {children}
      {title ? (
        <Text style={[buttonStyling.buttonText, { color: transparent ? 'black' : 'white' }]}>
          {title}
        </Text>
      ) : null}
    </TouchableOpacity>
  )
}

const buttonStyling = StyleSheet.create({
  button: {
    borderRadius: 10,
    padding: 5,
    display: 'flex',
    alignItems: 'center',
    gap: 3,
  },
  buttonText: {
    fontSize: 20,
    fontFamily: 'Poppins_400Regular',
    textAlign: 'center',
  },
})

export default CustomButton
