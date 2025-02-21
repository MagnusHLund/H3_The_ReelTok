import DropDownPicker from 'react-native-dropdown-picker'
import React, { useState } from 'react'
import { StyleSheet, useWindowDimensions } from 'react-native'
import Entypo from '@expo/vector-icons/Entypo'

interface DropDownProps {
  categories: { label: string; value: string }[]
  placeholder?: string
  widthProcentage?: number
}

const CustomDropdown: React.FC<DropDownProps> = ({
  categories,
  placeholder,
  widthProcentage = 0.8,
}) => {
  const { width } = useWindowDimensions()
  const [open, setOpen] = useState(false)
  const [value, setValue] = useState<string | null>(null)
  return (
    <DropDownPicker
      open={open}
      value={value}
      items={categories}
      setOpen={setOpen}
      setValue={setValue}
      setItems={() => {}}
      placeholder={placeholder}
      ArrowDownIconComponent={({ style }) => <Entypo name="chevron-down" size={24} color="white" />}
      ArrowUpIconComponent={({ style }) => <Entypo name="chevron-up" size={24} color="white" />}
      style={{
        backgroundColor: '#565656',
        borderRadius: 10,
        width: width * widthProcentage,
        // borderColor: 'white',
      }}
      textStyle={{
        fontSize: 15,
        color: 'white',
      }}
      containerStyle={{
        backgroundColor: '#565656',
        borderRadius: 10,
        width: width * widthProcentage,
      }}
      listChildContainerStyle={{
        backgroundColor: '#565656',
      }}
      arrowIconStyle={{
        width: 20,
        height: 20,
      }}
      tickIconStyle={{
        width: 20,
        height: 20,
      }}
      dropDownContainerStyle={{
        backgroundColor: '#696969',
        borderBlockColor: 'transparent',
        width: width * widthProcentage,
      }}
    />
  )
}

export default CustomDropdown
