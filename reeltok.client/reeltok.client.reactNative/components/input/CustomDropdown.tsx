import DropDownPicker from 'react-native-dropdown-picker';
import React, { useState } from 'react';
import { Text } from 'react-native';


interface DropDownProps {
  label: string;
  categories: { label: string; value: string }[];
  width?: number;
  height?: number;
}

const CustomDropdown: React.FC<DropDownProps> = ({ label, categories, width, height }) => {
  const [open, setOpen] = useState(false);
  const [value, setValue] = useState<string | null>(null);

  return (
    <>
      <Text>{label}</Text>
      <DropDownPicker
        open={open}
        value={value}
        items={categories}
        setOpen={setOpen}
        setValue={setValue}
        setItems={() => {}}
        placeholder='Select a category'
        style={{ width: width, height: height,borderRadius: 10 }}
        
      />
    </>
  );
};

export default CustomDropdown;
