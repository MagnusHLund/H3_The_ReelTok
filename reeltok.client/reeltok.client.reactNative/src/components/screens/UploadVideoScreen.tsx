import { useState } from "react";
import { useRoute } from "@react-navigation/native";
import { View, StyleSheet } from "react-native";
import Header from "../Layout/common/Header";
import useTranslation from "../../hooks/useTranslations";
import Navbar from "../Layout/common/Navbar";
import Title from "../Layout/common/Title";
import Section from "../Layout/common/Section";
import CustomTextInput from "../input/CustomTextInput";
import CustomDropdown, { DropdownOption } from "../input/CustomDropdown";
import CustomButton from "../input/CustomButton";

const UploadVideoScreen: React.FC = ({ }) => {
  const route = useRoute();
  const { video } = route.params; // For later use when we POST to the the Gateway.
  //const t = useTranslation();
  
  const [selectedCategory, setSelectedCategory] = useState<DropdownOption>();  
  
  const handleChangeCategory = (selectedCategory: DropdownOption) => {
    setSelectedCategory({label: selectedCategory.label, value: selectedCategory.value}) 
  };

  const categories: DropdownOption[] = [
    { label: 'Gaming', value: 'Gaming' },
    { label: 'Tech', value: 'Tech' },
    { label: 'Dance', value: 'Dance' },
    { label: 'Fight', value: 'Fight' },
    { label: 'Sport', value: 'Sport' },
    { label: 'Comedy', value: 'Comedy' },
  ];

  const defaultOption: DropdownOption = {
    label: 'Gaming', value: 'Gaming'
  };

  const styles = StyleSheet.create({
    videoScreenContainer: {
      height: '83.025%',
      width: '100%',
      display: 'flex',
      alignItems: 'center',
    }
  });

  return (
    <>
      <Header title={"Upload video"} />
      <View style={styles.videoScreenContainer}>
        <Section displayDivider={false}>
          <Title title="Title">
            <CustomTextInput placeholder="Title" />
          </Title>
        </Section>
        <Section displayDivider={false}>
          <Title title="Description">
            <CustomTextInput placeholder="Description" />
          </Title>
        </Section>
        <Section displayDivider={false}>
          <Title title="Category">
            <CustomDropdown defaultOption={defaultOption} options={categories} onChange={(selectedCategory: DropdownOption) => handleChangeCategory(selectedCategory)}/>
          </Title>
        </Section>
        <Section displayDivider={false}>
          <CustomButton widthPercentage={0.75} title="Upload" onPress={() => console.log("uploaded video")}/>
        </Section>
      </View>
      
      <Navbar />
    </>
  );
};

export default UploadVideoScreen;
