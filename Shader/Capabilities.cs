﻿namespace Shader
{
  public enum Capabilities
  {
    Matrix = 0,
    Shader = 1,
    Geometry = 2,
    Tessellation = 3,
    Addresses = 4,
    Linkage = 5,
    Kernel = 6,
    Vector16 = 7,
    Float16Buffer = 8,
    Float16 = 9,
    Float64 = 10,
    Int64 = 11,
    Int64Atomics = 12,
    ImageBasic = 13,
    ImageReadWrite = 14,
    ImageMipmap = 15,
    Pipes = 17,
    Groups = 18,
    DeviceEnqueue = 19,
    LiteralSampler = 20,
    AtomicStorage = 21,
    Int16 = 22,
    TessellationPointSize = 23,
    GeometryPointSize = 24,
    ImageGatherExtended = 25,
    StorageImageMultisample = 27,
    UniformBufferArrayDynamicIndexing = 28,
    SampledImageArrayDynamicIndexing = 29,
    StorageBufferArrayDynamicIndexing = 30,
    StorageImageArrayDynamicIndexing = 31,
    ClipDistance = 32,
    CullDistance = 33,
    ImageCubeArray = 34,
    SampleRateShading = 35,
    ImageRect = 36,
    SampledRect = 37,
    GenericPointer = 38,
    Int8 = 39,
    InputAttachment = 40,
    SparseResidency = 41,
    MinLod = 42,
    Sampled1D = 43,
    Image1D = 44,
    SampledCubeArray = 45,
    SampledBuffer = 46,
    ImageBuffer = 47,
    ImageMSArray = 48,
    StorageImageExtendedFormats = 49,
    ImageQuery = 50,
    DerivativeControl = 51,
    InterpolationFunction = 52,
    TransformFeedback = 53,
    GeometryStreams = 54,
    StorageImageReadWithoutFormat = 55,
    StorageImageWriteWithoutFormat = 56,
    MultiViewport = 57,
    SubgroupDispatch = 58,
    NamedBarrier = 59,
    PipeStorage = 60,
    GroupNonUniform = 61,
    GroupNonUniformVote = 62,
    GroupNonUniformArithmetic = 63,
    GroupNonUniformBallot = 64,
    GroupNonUniformShuffle = 65,
    GroupNonUniformShuffleRelative = 66,
    GroupNonUniformClustered = 67,
    GroupNonUniformQuad = 68,
    ShaderLayer = 69,
    ShaderViewportIndex = 70,
    SubgroupBallotKHR = 4423,
    DrawParameters = 4427,
    SubgroupVoteKHR = 4431,
    StorageBuffer16BitAccess = 4433,
    StorageUniformBufferBlock16 = 4433,
    StorageUniform16 = 4434,
    UniformAndStorageBuffer16BitAccess = 4434,
    StoragePushConstant16 = 4435,
    StorageInputOutput16 = 4436,
    DeviceGroup = 4437,
    MultiView = 4439,
    VariablePointersStorageBuffer = 4441,
    VariablePointers = 4442,
    AtomicStorageOps = 4445,
    SampleMaskPostDepthCoverage = 4447,
    StorageBuffer8BitAccess = 4448,
    UniformAndStorageBuffer8BitAccess = 4449,
    StoragePushConstant8 = 4450,
    DenormPreserve = 4464,
    DenormFlushToZero = 4465,
    SignedZeroInfNanPreserve = 4466,
    RoundingModeRTE = 4467,
    RoundingModeRTZ = 4468,
    Float16ImageAMD = 5008,
    ImageGatherBiasLodAMD = 5009,
    FragmentMaskAMD = 5010,
    StencilExportEXT = 5013,
    ImageReadWriteLodAMD = 5015,
    ShaderClockKHR = 5055,
    SampleMaskOverrideCoverageNV = 5249,
    GeometryShaderPassthroughNV = 5251,
    ShaderViewportIndexLayerEXT = 5254,
    ShaderViewportIndexLayerNV = 5254,
    ShaderViewportMaskNV = 5255,
    ShaderStereoViewNV = 5259,
    PerViewAttributesNV = 5260,
    FragmentFullyCoveredEXT = 5265,
    MeshShadingNV = 5266,
    ImageFootprintNV = 5282,
    FragmentBarycentricNV = 5284,
    ComputeDerivativeGroupQuadsNV = 5288,
    FragmentDensityEXT = 5291,
    ShadingRateNV = 5291,
    GroupNonUniformPartitionedNV = 5297,
    ShaderNonUniform = 5301,
    ShaderNonUniformEXT = 5301,
    RuntimeDescriptorArray = 5302,
    RuntimeDescriptorArrayEXT = 5302,
    InputAttachmentArrayDynamicIndexing = 5303,
    InputAttachmentArrayDynamicIndexingEXT = 5303,
    UniformTexelBufferArrayDynamicIndexing = 5304,
    UniformTexelBufferArrayDynamicIndexingEXT = 5304,
    StorageTexelBufferArrayDynamicIndexing = 5305,
    StorageTexelBufferArrayDynamicIndexingEXT = 5305,
    UniformBufferArrayNonUniformIndexing = 5306,
    UniformBufferArrayNonUniformIndexingEXT = 5306,
    SampledImageArrayNonUniformIndexing = 5307,
    SampledImageArrayNonUniformIndexingEXT = 5307,
    StorageBufferArrayNonUniformIndexing = 5308,
    StorageBufferArrayNonUniformIndexingEXT = 5308,
    StorageImageArrayNonUniformIndexing = 5309,
    StorageImageArrayNonUniformIndexingEXT = 5309,
    InputAttachmentArrayNonUniformIndexing = 5310,
    InputAttachmentArrayNonUniformIndexingEXT = 5310,
    UniformTexelBufferArrayNonUniformIndexing = 5311,
    UniformTexelBufferArrayNonUniformIndexingEXT = 5311,
    StorageTexelBufferArrayNonUniformIndexing = 5312,
    StorageTexelBufferArrayNonUniformIndexingEXT = 5312,
    RayTracingNV = 5340,
    VulkanMemoryModel = 5345,
    VulkanMemoryModelKHR = 5345,
    VulkanMemoryModelDeviceScope = 5346,
    VulkanMemoryModelDeviceScopeKHR = 5346,
    PhysicalStorageBufferAddresses = 5347,
    PhysicalStorageBufferAddressesEXT = 5347,
    ComputeDerivativeGroupLinearNV = 5350,
    CooperativeMatrixNV = 5357,
    FragmentShaderSampleInterlockEXT = 5363,
    FragmentShaderShadingRateInterlockEXT = 5372,
    ShaderSMBuiltinsNV = 5373,
    FragmentShaderPixelInterlockEXT = 5378,
    DemoteToHelperInvocationEXT = 5379,
    SubgroupShuffleINTEL = 5568,
    SubgroupBufferBlockIOINTEL = 5569,
    SubgroupImageBlockIOINTEL = 5570,
    SubgroupImageMediaBlockIOINTEL = 5579,
    IntegerFunctions2INTEL = 5584,
    SubgroupAvcMotionEstimationINTEL = 5696,
    SubgroupAvcMotionEstimationIntraINTEL = 5697,
    SubgroupAvcMotionEstimationChromaINTEL = 5698,
    Max = 0x7fffffff,
  }
}