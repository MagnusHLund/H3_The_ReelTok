import React from 'react'
import UploadVideo from '../LayoutTemp/common/UploadVideo'
import Header from '../LayoutTemp/common/Header'

const UploadVideoScreen = () => {
  return (
    <>
      <Header showBackButton title={'Upload video'} />
      <UploadVideo />
    </>
  )
}

export default UploadVideoScreen
