import React, { useState } from 'react'
import {
  View,
  Text,
  TouchableOpacity,
  FlatList,
  StyleSheet,
  useWindowDimensions,
} from 'react-native'

// TODO: Clean up this mess. I have been working on this garbage for so long, that it might not all be needed. Specifically styling
// TODO: Add arrow icon to dropdown

export type DropdownOption = {
  label: string
  value: string
}

interface DropdownProps {
  options: DropdownOption[]
  defaultOption?: DropdownOption
  widthPercentage?: number
  onChange: (selectedOption: DropdownOption) => void
}

const CustomPicker: React.FC<DropdownProps> = ({
  options,
  defaultOption = options[0],
  widthPercentage = 80,
  onChange,
}) => {
  const { width } = useWindowDimensions()
  const [selectedValue, setSelectedValue] = useState<DropdownOption>(defaultOption)
  const [listVisible, setListVisible] = useState(false)

  const handleSelect = (selectedOption: DropdownOption) => {
    setSelectedValue(selectedOption)
    setListVisible(false)
    onChange(selectedOption)
  }

  const calculatedWidth = width * widthPercentage

  return (
    <View style={[styles.container, { width: calculatedWidth / 100 }]}>
      <TouchableOpacity onPress={() => setListVisible(!listVisible)} style={styles.picker}>
        <Text style={styles.pickerText}>{selectedValue.label}</Text>
      </TouchableOpacity>
      {listVisible && (
        <View style={[styles.optionContainer, { width: calculatedWidth / 100 }]}>
          <FlatList
            data={options}
            keyExtractor={(item) => item.value}
            renderItem={({ item }) => (
              <TouchableOpacity onPress={() => handleSelect(item)} style={styles.option}>
                <Text style={styles.optionText}>{item.label}</Text>
              </TouchableOpacity>
            )}
          />
        </View>
      )}
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    borderColor: 'gray',
    borderWidth: 1,
    borderRadius: 10,
    justifyContent: 'flex-start',
    backgroundColor: 'white',
    display: 'flex',
    flexDirection: 'column',
  },
  picker: {
    height: 40,
    justifyContent: 'center',
    paddingLeft: 10,
  },
  pickerText: {
    color: 'black',
    fontSize: 14,
  },
  optionContainer: {
    position: 'absolute',
    top: 40,
    left: 0,
    right: 0,
    backgroundColor: 'white',
    borderColor: 'gray',
    borderWidth: 1,
    borderBottomLeftRadius: 10,
    borderBottomRightRadius: 10,
    zIndex: 10,
  },
  option: {
    backgroundColor: 'gray',
    paddingVertical: 10,
    paddingHorizontal: 20,
  },
  optionText: {
    color: 'black',
    fontSize: 14,
  },
})
export default CustomPicker
