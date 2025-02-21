import DropDownPicker from 'react-native-dropdown-picker'
import React, { useState } from 'react'
import { StyleSheet, useWindowDimensions } from 'react-native'
import Entypo from '@expo/vector-icons/Entypo'

interface DropDownProps {
  options: { label: string; value: string }[]
  placeholder?: string
  widthPercentage?: number
  onChange: () => void
}

const CustomDropdown: React.FC<DropDownProps> = ({
  options,
  placeholder,
  widthPercentage = 0.8,
  onChange,
}) => {
  const { width } = useWindowDimensions()
  const [open, setOpen] = useState(false)
  const [value, setValue] = useState<string | null>(null)

  const calculatedWidth = width * widthPercentage

  return (
    <DropDownPicker
      open={open}
      value={value}
      items={options}
      setOpen={setOpen}
      setValue={setValue}
      setItems={() => {}}
      placeholder={placeholder}
      ArrowDownIconComponent={() => <Entypo name="chevron-down" size={24} color="white" />}
      ArrowUpIconComponent={() => <Entypo name="chevron-up" size={24} color="white" />}
      style={[styles.dropdown, { width: calculatedWidth }]}
      textStyle={styles.text}
      containerStyle={[styles.container, { width: calculatedWidth }]}
      listChildContainerStyle={styles.listChildContainer}
      arrowIconStyle={styles.icon}
      tickIconStyle={styles.icon}
      dropDownContainerStyle={[styles.dropdownContainer, { width: calculatedWidth }]}
      onChangeValue={onChange}
    />
  )
}

const styles = StyleSheet.create({
  dropdown: {
    backgroundColor: '#565656',
    borderRadius: 10,
  },
  text: {
    fontSize: 15,
    color: 'white',
  },
  container: {
    backgroundColor: '#565656',
    borderRadius: 10,
  },
  listChildContainer: {
    backgroundColor: '#565656',
  },
  icon: {
    width: 20,
    height: 20,
  },
  dropdownContainer: {
    backgroundColor: '#696969',
    borderBlockColor: 'transparent',
  },
})

export default CustomDropdown
